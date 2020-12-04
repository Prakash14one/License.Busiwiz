<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="PublishUncompiled.aspx.cs" Inherits="Publish_Uncompiled_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <asp:UpdatePanel ID="pnlid" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="16px"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <asp:Panel ID="pnladdnew" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Select Product"></asp:Label>
                                         <asp:Label ID="Label8" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:DropDownList ID="ddlproductname" runat="server" Width="600px" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddlproductname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Select Code Type Category"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        
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
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Select Code Type"></asp:Label>
                                         <asp:Label ID="Label9" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcodetype"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlcodetype" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlcodetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="New Code Version Number"></asp:Label>
                                         <asp:Label ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:Label ID="lblnewcodetypeNo" runat="server" Text="New code version number"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Source Path Folder Name"></asp:Label>
                                         <asp:Label ID="Label11" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsourcepath"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtsourcepath" runat="server" 
                                        Width="400px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Temp Path for Compilation"></asp:Label>
                                         <asp:Label ID="Label14" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttemppath"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txttemppath" runat="server" Enabled="False" Width="400px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Output Path Folder Name"></asp:Label>
                                         <asp:Label ID="Label12" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtoutputsourcepath"
                                            ErrorMessage="*" ValidationGroup="1" ></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtoutputsourcepath" runat="server" Enabled="False" 
                                        Width="400px"></asp:TextBox>
                                    </label>
                                     
                                </td>
                            </tr>
                           
                          
                           
                                    <tr>
                                    <td colspan="2">
                                     <asp:Label ID="lblFTPCheck1" runat="server" Text="" 
                                            style="color: #000; font-size: 13px;"></asp:Label>
                                     <a href="ClientmasterFTPUpdate.aspx">ClientmasterFTPUpdate.aspx</a>
                                     <asp:Label ID="lblFTPCheck2" runat="server" Text="   " style="color: #000;font-size: 13px;"></asp:Label>
                                    </td>
                                    </tr>
                                   
                                    <tr>
                                    <td>
                                      <label>
                                      <asp:Label ID="Label15" runat="server" Text="FTP Server Name"></asp:Label>
                                     </label>
                                    </td>
                                    <td>
                                     
                                    <asp:Label ID="lblservername" runat="server" Text="" style="color: #000"></asp:Label>
                                       <a href="ClientmasterFTPUpdate.aspx" id="alinkup" runat="server">Update</a>
                                    </td>
                                    </tr>
                                    <tr>
                                   <td>
                                  <label>
                                    <asp:Label ID="Label16" runat="server" Text="User Name"></asp:Label>
                                    </label> 
                                   </td>
                                    <td>
                                    <asp:Label ID="lblusername" runat="server" Text="" style="color: #000"></asp:Label>
                                   
                                    </td>
                                    
                                    </tr>
                                  
                           
                           
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnSubmit" ValidationGroup="1" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegendd" runat="server" Text="List of compiled product"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnprint" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            CausesValidation="False" OnClick="btnprint_Click" />
                        <input id="btnin" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className = 'open'; javascript: CallPrint('divPrint'); document.getElementById('mydiv').className = 'closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Labeczcxl19" runat="server" Text="List of compiled product" Font-Italic="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdcompiledproduct" runat="server" CssClass="mGrid" GridLines="Both"
                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Product and Version" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproductidgrd" runat="server" Visible="false" Text='<%#Bind("ProductVersionId") %>'></asp:Label>
                                                    <asp:Label ID="lblproductnamegrd" runat="server" Text='<%#Bind("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodetypename" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Code Version No" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcodeversionno" runat="server" Text='<%#Bind("codeversionnumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Time" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldatetime" runat="server" Text='<%#Bind("DateTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="File Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfilename" runat="server" Text='<%#Bind("OutPutfileZipName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Output File Path" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfilepath" runat="server" Text='<%#Bind("OutputPath") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<%--asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>

