<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentCopyPaste.aspx.cs"
    Inherits="Account_DocumentCopyPaste" %>

<%@ Register Src="~/ioffice/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="../../css/StyleSheetifileview.css" rel="stylesheet" type="text/css" />
    <link href="../../css/MasterMain.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">



        function RealNumWithDecimal(myfield, e, dec) {

            //myfield=document.getElementById(FindName(myfield)).value
            //alert(myfield);
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            if (key == 13) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 27)) {
                return true;
            }
            else if ((("0123456789.").indexOf(keychar) > -1)) {
                return true;
            }
            // decimal point jump
            else if (dec && (keychar == ".")) {

                myfield.form.elements[dec].focus();
                myfield.value = "";

                return false;
            }
            else {
                myfield.value = "";
                return false;
            }
        }
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
   
        <table width="100%">
        <tr>
        <td>
         <asp:Panel ID="lblim" runat="server" Height="570px" Width="100%" ScrollBars="Both">
                <asp:DataList ID="DataList1" runat="server">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("image")%>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </asp:Panel>
        </td>
        </tr>
           
        </table>
        <table id="secondtbl1" cellspacing="0">
            <tr>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp; List of Documents to be paste to the following location
                    with the following details
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="#CC3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:Panel ID="asdvv" runat="server" Height="120px" ScrollBars="Both" Width="95%">
                        <asp:GridView ID="GridView2" runat="server" Width="95%" AutoGenerateColumns="False"
                            CssClass="GridBack" DataKeyNames="DocumentId" EmptyDataText="No one Folder Created .."
                            AllowPaging="false">
                            <Columns>
                                <asp:BoundField DataField="DocumentId" HeaderText="Doc ID" />
                                <asp:BoundField DataField="DocumentTitle" HeaderText="Document Title" />
                                <asp:BoundField DataField="PartyName" HeaderText="Party" />
                                <asp:BoundField DataField="DocumentMainType" HeaderText="Cabinet" />
                                <asp:BoundField DataField="DocumentSubType" HeaderText="Drawer" />
                                <asp:BoundField DataField="DocumentType" HeaderText="Folder" />
                                <asp:BoundField DataField="DocumentUploadDate" HeaderText="Date" />
                            </Columns>
                            <PagerStyle CssClass="GridPager" />
                            <HeaderStyle CssClass="GridHeader" />
                            <AlternatingRowStyle CssClass="GridAlternateRow" />
                            <RowStyle CssClass="GridRowStyle" />
                            <FooterStyle CssClass="GridFooter" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <table id="innertbl2" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        Copy/Move
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcopyormove" runat="server">
                                            <asp:ListItem Selected="True" Value="0">Copy</asp:ListItem>
                                            <asp:ListItem Value="1">Move</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="subcol1">
                                        Cabinet :
                                    </td>
                                    <td class="subcol2">
                                        <asp:DropDownList ID="ddlmaindoctype" runat="server" ValidationGroup="1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlmaindoclass_SelectedIndexChanged" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="subcol1">
                                        Drower :
                                    </td>
                                    <td class="subcol2">
                                        <asp:DropDownList ID="ddlsubdoctype" runat="server" ValidationGroup="1" OnSelectedIndexChanged="ddlsubdoctype_SelectedIndexChanged"
                                            AutoPostBack="True" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="subcol1">
                                        Folder :
                                    </td>
                                    <td class="subcol2">
                                        <asp:DropDownList ID="ddldoctype" runat="server" ValidationGroup="1" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="8">
                                        <asp:Button ID="imgbtnupdate" runat="server" Text="Paste" ValidationGroup="1" OnClick="imgbtnupdate_Click" />
                                        <input id="hdnDocId" name="HdnDocId" runat="server" type="hidden" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlmaindoctype" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlsubdoctype" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="imgbtnupdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
 
    </form>
</body>
</html>
