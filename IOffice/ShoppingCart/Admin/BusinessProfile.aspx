<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="BusinessProfile.aspx.cs" Inherits="CandidateApplicationRegistration"
    Title="Candiate Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024px,height=768px,toolbar=1,scrollbars=0,status=0');
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


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186 || evt.keyCode == 59) {


                alert("You have entered an invalid character");
                return false;
            }




        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
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
        .style1
        {
            height: 66px;
        }
    </style>
    <div style="clear: both;">
    </div>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="clear: both;">
            </div>
            <asp:Panel ID="pnlgrid" runat="server">
                <fieldset>
                    <div>
                        <table width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label88" runat="server" Text="Business Name : ">
                                        </asp:Label>
                                    </label>
                                </td>
                                <td style="width: 40%">
                                    <label>
                                        <asp:Label ID="lblwname" runat="server" Text="" Font-Bold="false"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbllink" runat="server" ForeColor="Black" 
                                        Text="View Slideshow" onclick="lbllink_Click"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Business Category : ">
                                        </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblcategory" runat="server" Text="" Font-Bold="false"></asp:Label>
                                    </label>
                                </td>
                                <td rowspan="3" valign="top">
                                    <asp:Image ID="imgLogo" runat="server" Height="130px" Width="120px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="In Business Since : ">
                                        </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblyears" runat="server" Text="" Font-Bold="false"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label31" runat="server" Text="No of Employees : ">
                                        </asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lblnos" runat="server" Text="" Font-Bold="false"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <fieldset>
                    <table width="100%">
                        <tr>
                            <td style="width: 20%" valign="top">
                                <label>
                                    <asp:Label ID="Label57" runat="server" Text="Title :"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lbldetails" runat="server" Font-Bold="false" Text="" ForeColor="#416271"
                                        Font-Size="14px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" valign="top">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Key Highlights :"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblkey" runat="server" Font-Bold="false" Text="" ForeColor="#416271"
                                        Font-Size="14px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" valign="top">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Corporate Info :"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblcorporate" runat="server" Font-Bold="false" Text="" ForeColor="#416271"
                                        Font-Size="14px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" valign="top">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Facts :"></asp:Label>
                                </label>
                            </td>
                            <td>
                                <label>
                                    <asp:Label ID="lblfact" runat="server" Font-Bold="false" Text="" ForeColor="#416271"
                                        Font-Size="14px"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="lblAddresslbl" runat="server" Text="Business Contact Details : "></asp:Label>
                                </label>
                            </td>
                            <td valign="bottom">
                                <asp:Label ID="Label18" runat="server" Text="" ForeColor="#416271" Font-Size="14px"></asp:Label>,
                                <asp:Label ID="Label17" runat="server" Text="" ForeColor="#416271" Font-Size="14px"></asp:Label>,
                                <asp:Label ID="Label16" runat="server" Text="" ForeColor="#416271" Font-Size="14px"></asp:Label>
                            </td>
                            </td>
                        </tr>
                        <asp:Panel ID="pandkee" runat="server" Visible="false">
                            <tr>
                                <td style="width: 20%">
                                </td>
                                <td valign="bottom">
                                    <asp:Label ID="lbladdress" runat="server" Text="" Font-Bold="false" ForeColor="#416271"
                                        Font-Size="14px"></asp:Label>
                                    <asp:Label ID="tbZipCode" runat="server" Text="" ForeColor="#416271" Font-Size="14px"></asp:Label>
                            </tr>
                        </asp:Panel>
                    </table>
                </fieldset>
            </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
