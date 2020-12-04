<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDocument.aspx.cs" Inherits="Account_ViewDocument" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link rel="stylesheet" type="text/css" href="Extcss/style.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Extcss/forms.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Extcss/fieldsets.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Extcss/info.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="Extcss/formulaire-css3.css" media="screen" />

    <script language="JavaScript" type="text/javascript">
   
    </script>

    <style type="text/css">
        #imagediv
        {
            margin: 0 auto;
        }
        img
        {
            position: static;
            width: 100%;
        }
    </style>

    <script type="text/javascript" language="javascript">
window.onload = function(){zoom(1)}
function zoom(zm) {
img=document.getElementById('<%= Image2.ClientID %>')
wid=img.width
ht=img.height
img.style.width=(wid*zm)+"px"
img.style.height=(ht*zm)+"px"
//img.style.marginLeft = -(img.width/2) + "px";
//img.style.marginTop = -(img.height/2) + "px";
}
    </script>

</head>
<body>
    <form id="horizontalForm" runat="server" class="horizontalForm">
    <div id="main_container">
        <div id="main_content">
            <div id="right_content">
                <div class="products_box">
                    <table id="doctbl1" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="6">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="14px" Text="Document Tree"></asp:Label>
                                            <asp:LinkButton ID="lnkbtnShowTree" Font-Bold="true" Font-Size="14px" runat="server"
                                                OnClick="lnkbtnShowTree_Click" Font-Underline="True">Show</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnHideTree" runat="server" Font-Bold="true" Font-Size="14px"
                                                Visible="False" OnClick="lnkbtnHideTree_Click">Hide</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Button ID="imgbtnSave" runat="server" Text="Save" OnClick="imgbtnSave_Click" />
                                            <asp:Button ID="imgbtnPrint" runat="server" Text="Print" OnClick="imgbtnPrint_Click" />
                                            <asp:Button ID="imgbtnSendMessage" runat="server" Text="Send Message" OnClick="imgbtnSendMessage_Click" />
                                            <asp:Button ID="ImgBtnEmail" runat="server" Text="Email" OnClick="ImgBtnEmail_Click" />
                                            <asp:Button ID="ImgBtnFax" runat="server" Text="Fax" Visible="false" OnClick="ImgBtnFax_Click" />
                                            <asp:Button ID="ImageButton2" Visible="false" runat="server" Text="Back" OnClick="ImageButton2_Click" />
                                            <asp:Button ID="btnOK" runat="server" OnClientClick="window.close(); return false;"
                                                Text="Close" />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton6" runat="server" Font-Bold="true" Font-Size="14px"
                                                CausesValidation="false" OnClick="LinkButton6_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton4" runat="server" Font-Bold="true" Font-Size="14px"
                                                CausesValidation="false" OnClick="LinkButton4_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Page No."></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                Display="Dynamic" ControlToValidate="lblnooff" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="lblnooff" ValidChars="0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:TextBox ID="lblnooff" runat="server" Width="35px">1</asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblfrom" runat="server" Text="Of"></asp:Label>
                                            <asp:Label ID="lblnototal" Text="1" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="imgimgo" runat="server" ValidationGroup="1" OnClick="imgimgo_Click"
                                                Text="Go" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgfirstimg" runat="server" ValidationGroup="1" Width="20px"
                                                Height="20px" ImageUrl="~/Account/images/firstpg.gif" AlternateText="First" OnClick="imgfirstimg_Click" />
                                            <asp:ImageButton ID="imgpriimg" runat="server" ValidationGroup="1" Width="20px" Height="20px"
                                                ImageUrl="~/Account/images/prevpg.gif" AlternateText="Privious" OnClick="imgpriimg_Click" />
                                            <asp:ImageButton ID="imgnextimg" runat="server" ValidationGroup="1" Width="20px"
                                                Height="20px" ImageUrl="~/Account/images/nextpg.gif" AlternateText="Next" OnClick="imgnextimg_Click" />
                                            <asp:ImageButton ID="imglastimg" runat="server" ValidationGroup="1" Width="20px"
                                                Height="20px" ImageUrl="~/Account/images/lastpg.gif" AlternateText="Last" OnClick="imglastimg_Click" />
                                        </td>
                                        <td>
                                            <input type="button" value="-" title="Zoom Out" style="width: 30px;" onclick="zoom(0.9)" />
                                            <input type="button" value="+" title="Zoom In"  style="width: 30px;" onclick="zoom(1.1)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="true" CausesValidation="false"
                                                OnClick="LinkButton2_Click"></asp:LinkButton>
                                            <asp:Label ID="Label4" runat="server" Text=" >>"></asp:Label>
                                            <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="true" CausesValidation="false"
                                                OnClick="LinkButton3_Click"></asp:LinkButton>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text=" >>"></asp:Label>
                                            <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="true" CausesValidation="false"
                                                OnClick="LinkButton5_Click"></asp:LinkButton>
                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text=" >>"></asp:Label>
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lnkbtnShowRelated" runat="server" Visible="false" OnClick="lnkbtnShowRelated_Click">Show</asp:LinkButton>
                                            <asp:LinkButton ID="lnkbtnHideRelated" runat="server" Visible="False" OnClick="lnkbtnHideRelated_Click">Hide</asp:LinkButton>
                                            <asp:Label ID="lblddcname" Visible="false" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table width="100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel ID="pnltree" Visible="false" runat="server" Width="200px" Height="768px" ScrollBars="Both"
                                                CssClass="divpnl2">
                                                <table id="Table1">
                                                    <tr>
                                                        <td valign="top" >
                                                            <asp:TreeView ID="tree1" runat="server" Font-Bold="true" OnSelectedNodeChanged="tree1_SelectedNodeChanged"
                                                                >
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td valign="top">
                                            <asp:Panel ID="pnlDoc" Width="1000px"  Height="100%" ScrollBars="Horizontal" runat="server">
                                                <div id="imagediv" style="text-align:center;">
                                                    <asp:Image ID="Image2" Width="75%" ImageAlign="Middle"  Height="75%" runat="server" ImageUrl='<%#Eval("image")%>' />
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel CssClass="divpnl1" ID="pnlRelated" Visible="false" runat="server">
                        <table id="divtbl">
                            <tr>
                                <td class="text">
                                    <asp:DataList ID="DataListFolder" runat="server" OnItemCommand="DataListFolder_ItemCommand"
                                        DataKeyField="FolderID">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td colspan="2" style="height: 20px">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Account/images/closeFolder.jpg" />
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("FolderName") %>'
                                                            Font-Bold="True" ForeColor="#996600"></asp:LinkButton>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:GridView ID="Gridreqinfo" runat="server" AutoGenerateColumns="False" DataKeyNames="DocumentId"
                                                            OnRowCommand="Gridreqinfo_RowCommand" PageSize="7" GridLines="None" ShowHeader="False">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Image ID="Image3" runat="server" Height="21px" ImageUrl="~/Account/images/pdf_icon.png"
                                                                            Width="17px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:ButtonField CommandName="view" DataTextField="DocumentTitle" Text="Button" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                        <%--</div> --%>
                    </asp:Panel>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Panel ID="pnloa" runat="server" BackColor="White" BorderColor="#999999" Width="900px"
                                    ScrollBars="None" BorderStyle="Solid" BorderWidth="4px">
                                    <table width="100%">
                                        <tr>
                                            <asp:ScriptManager ID="spManager" runat="server">
                                            </asp:ScriptManager>
                                            <td>
                                                <label>
                                                    Accounting Entries done for following document
                                                </label>
                                            </td>
                                            <td align="right">
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Account/images/closeicon.png" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    Doc ID :
                                                    <asp:Label ID="lbldid" runat="server" Text=""></asp:Label>
                                                </label>
                                                <label>
                                                    Doc Title :
                                                    <asp:Label ID="lbldtitle" runat="server" Text=""></asp:Label>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:UpdatePanel ID="pvb" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>
                                                            <asp:RadioButtonList ID="rdradio" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                OnSelectedIndexChanged="rdradio_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" Text="Make new entry"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Add to Existing Entry" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="rdradio" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    <asp:Panel ID="pvlnewentry" runat="server" Visible="false">
                                                        <asp:DropDownList ID="ddloa" runat="server" Width="200px">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="ImageButton5" runat="server" Text=" Go " OnClick="ImageButton5_Click" />
                                                        <asp:HyperLink ID="hypost" Visible="false" runat="server" Target="_blank" />
                                                    </asp:Panel>
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlexist" runat="server" Visible="true">
                                                    <asp:DropDownList ID="ddldo" runat="server" Width="200px">
                                                        <asp:ListItem Text="Cash Register"></asp:ListItem>
                                                        <asp:ListItem Text="Journal Register"></asp:ListItem>
                                                        <asp:ListItem Text="Cr/Dr Note Register"></asp:ListItem>
                                                        <asp:ListItem Text="Packing Slip Register"></asp:ListItem>
                                                        <asp:ListItem Text="Purechase Register"></asp:ListItem>
                                                        <asp:ListItem Text="Sales Register"></asp:ListItem>
                                                        <asp:ListItem Text="Sales Order Register"></asp:ListItem>
                                                        <asp:ListItem Text="Expense Register"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="img2" runat="server" OnClick="Img2_Click" Text=" Go " />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <label>
                                                    List of accounting entries done based on this document.
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel5" runat="server" ScrollBars="Both" Height="230px" Width="100%">
                                                    <asp:GridView ID="gridpopup" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                        GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                        EmptyDataText="No Record Found." OnSelectedIndexChanged="gridpopup_SelectedIndexChanged"
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Datetime" HeaderText="Date" />
                                                            <asp:BoundField DataField="Entry_Type_Name" HeaderText="Entry Type" />
                                                            <asp:BoundField DataField="EntryNumber" HeaderText="Entry Number" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="mdloa" BackgroundCssClass="modalBackground" PopupControlID="pnloa"
                                    TargetControlID="Hidden1" CancelControlID="ImageButton6" runat="server">
                                </cc1:ModalPopupExtender>
                                <input id="Hidden1" name="Hidden1" runat="Server" type="hidden" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
