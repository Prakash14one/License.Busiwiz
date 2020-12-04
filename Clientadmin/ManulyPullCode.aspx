<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ManulyPullCode.aspx.cs" Inherits="ManulyPullCode" Title="" %>

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
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Server Client"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button1_Click1" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select Server"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlserver" runat="server" >
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Select Product/Version"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="FilterProductname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label4" runat="server" Text="Select Code Type"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlcodetype" runat="server">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Button ID="Button2" runat="server" Text="Go"  OnClick="Button2_Click1" /></label>
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
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List Of Server Client"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Font-Italic="true" Text="Server :"></asp:Label>
                                                                <asp:Label ID="lblserv" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label22" runat="server" Font-Italic="true" Text="Products/version :"></asp:Label>
                                                                <asp:Label ID="lblproductname" runat="server" Font-Italic="true"></asp:Label>
                                                                &nbsp;
                                                                <asp:Label ID="Label23" runat="server" Font-Italic="true" Text="Code Type :"></asp:Label>
                                                                <asp:Label ID="lblcodetype" runat="server" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="ID"
                                                    EmptyDataText="No Record Found." AllowSorting="True" 
                                                    OnSorting="GridView1_Sorting" onrowdatabound="GridView1_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Server Name" SortExpression="ServerName" HeaderStyle-HorizontalAlign="Left"
                                                            >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblservermaster" runat="server" Text='<%#Bind("ServerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Product-Version" SortExpression="VersionInfoName"
                                                            HeaderStyle-HorizontalAlign="Left"  >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproductversioninfo" runat="server" Text='<%#Bind("VersionInfoName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Version Number" SortExpression="codeversionnumber"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcno" runat="server" Text='<%#Bind("codeversionnumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" SortExpression="filename" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfname" runat="server" Text='<%#Bind("filename") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload Status" SortExpression="Successfullyuploadedtoserver"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblupsuc" runat="server" Text='<%#Bind("Successfullyuploadedtoserver") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transfer Code" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                            <asp:Label ID="lblbusicontrolsiteurl" Visible="false" runat="server" Text='<%#Bind("Busiwizsatellitesiteurl") %>'></asp:Label>

                                                            <asp:Label ID="lblserverid" runat="server" Visible="false" Text='<%#Bind("ServerID") %>'></asp:Label>
                                                            <asp:Label ID="lblproductlatestcodeid" Visible="false" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                                                                <asp:Button ID="btntransfercode" OnClick="btntransfercode_Click" runat="server" Text="Transfer Code" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
