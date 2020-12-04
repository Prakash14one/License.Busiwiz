<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="CodeType.aspx.cs" Inherits="CodeType" Title="" %>

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
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="Product Update"></asp:Label>
                    </legend>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Products Version"></asp:Label>
                                        <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductversion" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlproductversion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Category Type "></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcodetypecategory" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Default Category Type"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownList1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Code Type Category"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlproductversion"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtcodetypecategory" runat="server"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                              <tr>
                                <td style="width: 30%">
                                   
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit"
                                        ValidationGroup="1" Text="Submit" onclick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label10" runat="server" Text="List Of Product Update"></asp:Label>
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
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" Text="List of Code Type" ForeColor="Black"
                                                                    Font-Italic="true"></asp:Label>
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
                                                    EmptyDataText="No Record Found." AllowSorting="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product/Version" SortExpression="VersionInfoName"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("VersionInfoName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Code Type Category " SortExpression="CodeTypeCategory" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodetypecategory" runat="server" Text='<%#Bind("CodeTypeCategory") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                         <asp:TemplateField HeaderText="Default Code Name " SortExpression="CodeTypeName" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldefaultcodename" runat="server" Text='<%#Bind("CodeTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Code Type" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="30%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodetype" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
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
    </asp:UpdatePanel>
</asp:Content>
