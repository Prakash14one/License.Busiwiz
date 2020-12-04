<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="ProductUpdate.aspx.cs" Inherits="ProductUpdateLastt" Title="" %>

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
                        <asp:Label ID="lbllegend" runat="server" Text="Product Update"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="addnewpanel" runat="server" Text="Add New" CssClass="btnSubmit" OnClick="addnewpanel_Click" />
                         <asp:Button ID="btndosyncro" runat="server" CssClass="btnSubmit"  OnClick="btndosyncro_Clickpop" Text="Do Synchronise" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%" Visible="false">
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
                                        onselectedindexchanged="ddlproductversion_SelectedIndexChanged" Width="400px" >
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Select Code Type Category"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcodetypecatefory"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcodetypecatefory" runat="server" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddlcodetypecatefory_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select Code Type for Update"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcodtypeforup"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlcodtypeforup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcodtypeforup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="New Code Version Number"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:Label ID="lblnewcodetypeNo" runat="server" Text="New code version number"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Select File to Upload"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:FileUpload ID="fileup" runat="server" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                </td>
                                <td style="width: 70%">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btnSubmit"
                                        ValidationGroup="1" />
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
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Filter by Product/Version"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="FilterProductname" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="FilterProductname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label ID="Label16" runat="server" Text="Code Type"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlctype" runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Button ID="Button2" runat="server" Text="Go" CssClass="btnSubmit" OnClick="Button2_Click1" /></label>
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
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="list of product update"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
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
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Width="100%" DataKeyNames="Id"
                                                    EmptyDataText="No Record Found." AllowSorting="True" OnSorting="GridView1_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Product/Version" SortExpression="VersionInfoName"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="11%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpversion" runat="server" Text='<%#Bind("VersionInfoName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code Type" SortExpression="CodeType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="18%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBackColor" runat="server" Text='<%#Bind("CodeType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code Version" SortExpression="CodeVersion" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="5%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcodeversion" runat="server" Text='<%#Bind("CodeVersion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Name" SortExpression="FileName" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("FileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File Location" SortExpression="FileLocation" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilelocation" runat="server" Text='<%#Bind("FileLocation") %>'></asp:Label>
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

                        <tr>
                            <td>
                              <asp:Panel ID="Paneldoc" runat="server" Width="55%" CssClass="modalPopup">
                                    <fieldset>
                                        <legend>
                                            <asp:Label ID="Label142" runat="server" Text=""></asp:Label>
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
                                                        <asp:Label ID="Label143" runat="server" Text="Was this the last record you are going to add right now to this table?"></asp:Label>
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
            <asp:PostBackTrigger ControlID="Button1" />
            <%-- <asp:PostBackTrigger ControlID="addnewpanel" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
